using Minio;
using Minio.Exceptions;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using Minio.DataModel.Args;

public class MinioService
{
    private readonly IAmazonS3 _s3Client;

    private readonly MinioClient _minioClient;
    private readonly string _endpoint;
    private readonly string _bucketName;

    public MinioService(IConfiguration configuration)
    {
        var minioConfig = configuration.GetSection("MinIO");
        var endpoint = minioConfig["Endpoint"];
        var accessKey = minioConfig["AccessKey"];
        var secretKey = minioConfig["SecretKey"];
        _bucketName = minioConfig["BucketName"];

    
        var useSSL = false;

        _minioClient = (MinioClient?)new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .WithSSL(useSSL)
            .Build();

        CreateBucketAsync();    

        /*
        var s3Config = new AmazonS3Config
        {
            ServiceURL = endpoint,
            ForcePathStyle = true, // Required for MinIO
            RegionEndpoint = RegionEndpoint.USEast1 // Can be any region
        };

        _s3Client = new AmazonS3Client(accessKey, secretKey, s3Config);*/
    }

    public async Task CreateBucketAsync()
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

    public async Task UploadFileAsync( string objectName, Stream fileStream)
    {
        try
        {
            // Check if the bucket exists
            var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
            if (!bucketExists)
            {
                throw new Exception($"Bucket '{_bucketName}' does not exist.");
            }

            // Upload the file to the bucket
            await _minioClient.PutObjectAsync(
                new PutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithStreamData(fileStream)
                    .WithObjectSize(fileStream.Length)
                    .WithContentType("application/octet-stream") // Set the content type
            );

            Console.WriteLine($"File '{objectName}' uploaded successfully to bucket '{_bucketName}'.");
        }
        catch (MinioException ex)
        {
            Console.WriteLine($"Error uploading file: {ex.Message}");
            throw;
        }
    }

    public async Task<MemoryStream> DownloadFileAsync( string objectName)
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
            throw;
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