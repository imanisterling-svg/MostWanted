using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MostWanted.Model;
using MostWanted.Services;
using MostWanted.ViewsModels;

namespace MostWanted.ViewModels;

public partial class ReportSpottedViewModel(SpottedService service) : BaseViewModel
{
    private readonly SpottedService _service = service;

    public WantedPerson WantedPerson { get; set; }

    [ObservableProperty]
    private  string notes;

    private string? mediaPath;
    private string? mediaType;

    [RelayCommand]
    private async Task ReportSpotted()
    {
        var action = await Shell.Current.DisplayActionSheet(
            "Report Spotted", "Cancel", null, "Take Photo", "Record Video");

        if (action == "Take Photo") await TakePhoto();
        else if (action == "Record Video") await RecordVideo();
    }

    [RelayCommand]
    private static async Task TakePhoto()
    {
        var photo = await MediaPicker.Default.CapturePhotoAsync();
        if (photo == null) return;

        var mediaPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
        using var stream = await photo.OpenReadAsync();
        using var newStream = File.OpenWrite(mediaPath);
        await stream.CopyToAsync(newStream);
    }

    [RelayCommand]
    private static async Task RecordVideo()
    {
        var video = await MediaPicker.Default.CaptureVideoAsync();
        if (video == null) return;

        var mediaPath = Path.Combine(FileSystem.CacheDirectory, video.FileName);
        using var stream = await video.OpenReadAsync();
        using var newStream = File.OpenWrite(mediaPath);
        await stream.CopyToAsync(newStream);
    }

    [RelayCommand]
    private async Task Submit()
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync()
                           ?? await Geolocation.GetLocationAsync();

            if (location == null)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Location unavailable", "OK");
                return;
            }

            var spotted = new Spotted
            {
                WantedPersonId = WantedPerson.Id,
                Name = WantedPerson.Name,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                DateSpotted = DateTime.Now,
                Notes = Notes,
                MediaPath = mediaPath,
                MediaType = mediaType
            };

            await _service.SaveSpottedAsync(spotted);
            await Shell.Current.DisplayAlertAsync("Success", "Report submitted!", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }
}
