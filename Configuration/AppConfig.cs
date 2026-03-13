using System.Text.Json.Serialization;

namespace GameLibrary.Configuration
{
  public class AppConfig
  {
    public required string DownloadPath { get; set; }
    public double MaxDownloadSpeed { get; set; }
    public bool EnableAutoUpdates { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Theme CurrentTheme { get; set; }

    public enum Theme
    {
      Light,
      Dark,
      System
    }
  }
}