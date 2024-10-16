using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PinController : ControllerBase
{
    private readonly string _folderPath = Directory.GetCurrentDirectory();
    private readonly string _filePath;

    public PinController()
    {
        if (!Directory.Exists(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);
        }

        _filePath = Path.Combine(_folderPath, "Data.txt");
    }

    [HttpPost("SavePin")]
    public async Task<IActionResult> SavePin([FromBody] PinData pinData)
    {
        if (pinData == null)
        {
            return BadRequest("Invalid data.");
        }

        var pinInfo = $"Location: {pinData.Latitude},{pinData.Longitude}\n" +
                      $"Description: {pinData.Description}\n" +
                      $"Photo: {pinData.PhotoFileName}\n\n";

        await System.IO.File.AppendAllTextAsync(_filePath, pinInfo);

        return Ok("Pin data saved successfully.");
    }
}

public class PinData
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Description { get; set; }
    public string PhotoFileName { get; set; }
}
