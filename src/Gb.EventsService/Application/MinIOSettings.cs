﻿namespace Application;

public class MinIOSettings
{
    public string Endpoint {  get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string BucketName { get; set; }
    public bool UseSSL { get; set; }
}
