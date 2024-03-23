using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Logic.Handlers.Loki.Entites
{
    public class Cache
    {
        public Chunk chunk { get; set; }
        public Index index { get; set; }
        public Result result { get; set; }
        public StatsResult statsResult { get; set; }
    }

    public class Chunk
    {
        public int headChunkBytes { get; set; }
        public int headChunkLines { get; set; }
        public int decompressedBytes { get; set; }
        public int decompressedLines { get; set; }
        public int compressedBytes { get; set; }
        public int totalDuplicates { get; set; }
        public int postFilterLines { get; set; }
        public int headChunkStructuredMetadataBytes { get; set; }
        public int decompressedStructuredMetadataBytes { get; set; }
        public int entriesFound { get; set; }
        public int entriesRequested { get; set; }
        public int entriesStored { get; set; }
        public int bytesReceived { get; set; }
        public int bytesSent { get; set; }
        public int requests { get; set; }
        public int downloadTime { get; set; }
    }

    public class Data
    {
        public string resultType { get; set; }
        public List<Result> result { get; set; }
        public Stats stats { get; set; }
    }

    public class Index
    {
        public int entriesFound { get; set; }
        public int entriesRequested { get; set; }
        public int entriesStored { get; set; }
        public int bytesReceived { get; set; }
        public int bytesSent { get; set; }
        public int requests { get; set; }
        public int downloadTime { get; set; }
    }

    public class Ingester
    {
        public int totalReached { get; set; }
        public int totalChunksMatched { get; set; }
        public int totalBatches { get; set; }
        public int totalLinesSent { get; set; }
        public Store store { get; set; }
    }

    public class Querier
    {
        public Store store { get; set; }
    }

    public class Result
    {
        public Stream stream { get; set; }
        public List<List<string>> values { get; set; }
        public int entriesFound { get; set; }
        public int entriesRequested { get; set; }
        public int entriesStored { get; set; }
        public int bytesReceived { get; set; }
        public int bytesSent { get; set; }
        public int requests { get; set; }
        public int downloadTime { get; set; }
    }

    public class GetLogsResponse
    {
        public string status { get; set; }
        public Data data { get; set; }
    }

    public class Stats
    {
        public Summary summary { get; set; }
        public Querier querier { get; set; }
        public Ingester ingester { get; set; }
        public Cache cache { get; set; }
    }

    public class StatsResult
    {
        public int entriesFound { get; set; }
        public int entriesRequested { get; set; }
        public int entriesStored { get; set; }
        public int bytesReceived { get; set; }
        public int bytesSent { get; set; }
        public int requests { get; set; }
        public int downloadTime { get; set; }
    }

    public class Store
    {
        public int totalChunksRef { get; set; }
        public int totalChunksDownloaded { get; set; }
        public int chunksDownloadTime { get; set; }
        public Chunk chunk { get; set; }
    }

    public class Stream
    {
        public string AppId { get; set; }
        public string Message { get; set; }
        public string _Id { get; set; }
        public string data_provider { get; set; }
        public string TEST { get; set; }
        public string LogLevel { get; set; }
        public string date { get; set; }
    }

    public class Summary
    {
        public int bytesProcessedPerSecond { get; set; }
        public int linesProcessedPerSecond { get; set; }
        public int totalBytesProcessed { get; set; }
        public int totalLinesProcessed { get; set; }
        public double execTime { get; set; }
        public double queueTime { get; set; }
        public int subqueries { get; set; }
        public int totalEntriesReturned { get; set; }
        public int splits { get; set; }
        public int shards { get; set; }
        public int totalPostFilterLines { get; set; }
        public int totalStructuredMetadataBytesProcessed { get; set; }
    }
}
