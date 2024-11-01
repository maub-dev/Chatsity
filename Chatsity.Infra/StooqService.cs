using System.Globalization;

namespace Chatsity.Infra
{
    public class StockData
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public long Volume { get; set; }
    }

    public class StooqService
    {
        private static readonly HttpClient _httpClient = new();

        public static async Task<StockData> GetStockDataAsync(string stockCode)
        {
            var url = $"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv";

            // Send GET request to retrieve CSV data
            var response = await _httpClient.GetStringAsync(url);

            using (var reader = new StringReader(response))
            {
                // Skip header line
                reader.ReadLine();

                // Read data line
                var line = reader.ReadLine();
                var values = line.Split(',');

                return new StockData
                {
                    Symbol = values[0],
                    Date = DateTime.ParseExact(values[1], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Time = TimeSpan.Parse(values[2]),
                    Open = decimal.Parse(values[3], NumberStyles.Number),
                    High = decimal.Parse(values[4], NumberStyles.Number),
                    Low = decimal.Parse(values[5], NumberStyles.Number),
                    Close = decimal.Parse(values[6], NumberStyles.Number),
                    Volume = long.Parse(values[7], NumberStyles.Number)
                };
            }
        }
    }
}
