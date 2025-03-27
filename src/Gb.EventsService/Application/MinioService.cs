using Minio;
using Minio.Exceptions;
using Amazon.S3;
using Minio.DataModel.Args;
using Application;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.Extensions.Logging;

public class MinioService
{
    //private readonly IAmazonS3 _s3Client;
    private readonly MinioClient _minioClient;
    //private readonly string _endpoint;
    private readonly string _bucketName;
    private readonly MinIOSettings _settings;
    private readonly ILogger<MinioService> _logger;

    public MinioService(IOptions<MinIOSettings> options, ILogger<MinioService> logger)
    {
        _logger = logger;
        _settings = options.Value;
        var endpoint = _settings.Endpoint;
        var accessKey = _settings.AccessKey;
        var secretKey = _settings.SecretKey;
        _bucketName = _settings.BucketName;

        _minioClient =
            (MinioClient)new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .WithSSL(_settings.UseSSL)
            .Build();

        EnsureBucketCreatedAsync().GetAwaiter().GetResult();

        /*
        var s3Config = new AmazonS3Config
        {
            ServiceURL = endpoint,
            ForcePathStyle = true, // Required for MinIO
            RegionEndpoint = RegionEndpoint.USEast1 // Can be any region
        };

        _s3Client = new AmazonS3Client(accessKey, secretKey, s3Config);*/
    }

    public async Task<string?> UploadFileAsync(int eventId, string objectName, string contentType, Stream fileStream)
    {
        try
        {
            if (fileStream.Length <= 0)
            {
                _logger.LogInformation($"File stream was empty for a '{objectName}' file");
                return null;
            }

            // Upload the file to the bucket
            var response = await _minioClient.PutObjectAsync(
                new PutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithStreamData(fileStream)
                    .WithObjectSize(fileStream.Length)
                    .WithHeaders(new Dictionary<string, string>() { { "event-id", eventId.ToString() } })
                    //.WithContentType("application/octet-stream") // Set the content type
                    .WithContentType(contentType) // Set the content type
            );

            if (response.ResponseStatusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogInformation(response.ResponseContent, "S3 Storage returned a 400 status");
                return null;
            }

            _logger.LogInformation($"File '{objectName}' uploaded successfully to bucket '{_bucketName}'.");
            return response.Etag;
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, "Error occured while uploading a file");
            return null;
        }
    }

    public async Task<string?> IssuePresignedUrlForUpload(string objectName)
    {
        try
        {
            var presignedUrl = await _minioClient.PresignedPutObjectAsync(
                new PresignedPutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithExpiry(60 * 60) // an hour
            );

            return presignedUrl;
        }
        catch (MinioException ex)
        {
            Console.WriteLine($"Error uploading file: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> IssuePresignedUrlForDownload(int eventId, string objectName)
    {
        try
        {
            _logger.LogInformation($"Trying to receive a presigned URL for the file '{objectName}'");

            var presignedUrl = await _minioClient.PresignedPutObjectAsync(
                new PresignedPutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithExpiry(60 * 60) // an hour
                    .WithHeaders(new Dictionary<string, string>() { { "event-id", eventId.ToString() } })
            );

            _logger.LogInformation("Received a presigned url: " + presignedUrl);

            return presignedUrl;
        }
        catch (MinioException ex)
        {
            _logger.LogError($"Error getting a presigned URL for a file download: {ex.Message}");
            return null;
        }
    }

    public async Task<MemoryStream?> DownloadFileAsync(string objectName)
    {
        try
        {
            // Check if the bucket exists
            var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
            if (!bucketExists)
            {
                throw new Exception($"Bucket '{_bucketName}' does not exist.");
            }

            // Download the file from the bucket
            var memoryStream = new MemoryStream();
            await _minioClient.GetObjectAsync(
                new GetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin); // Reset the stream position
                    })
            );

            Console.WriteLine($"File '{objectName}' downloaded successfully from bucket '{_bucketName}'.");
            return memoryStream;
        }
        catch (MinioException ex)
        {
            Console.WriteLine($"Error downloading file: {ex.Message}");
        }
        return null;
    }

    public async Task GetPresignUrlAsync(PresignedGetObjectArgs args)
    {
        var url = await _minioClient.PresignedGetObjectAsync(args);
    }

    private async Task EnsureBucketCreatedAsync()
    {
        try
        {
            // Check if the bucket already exists
            var exists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
            if (!exists)
            {
                // Create the bucket if it doesn't exist
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
                Console.WriteLine($"Bucket '{_bucketName}' created successfully.");
            }
        }
        catch (MinioException ex)
        {
            Console.WriteLine($"Error creating bucket: {ex.Message}");
        }
    }

    /*
    public async Task UploadFileAsync(string objectName, Stream fileStream)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = objectName,
            InputStream = fileStream
        };

        await _s3Client.PutObjectAsync(putRequest);
    }

    public async Task<Stream> DownloadFileAsync(string objectName)
    {
        var getRequest = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = objectName
        };

        var response = await _s3Client.GetObjectAsync(getRequest);
        return response.ResponseStream;
    }*/
}