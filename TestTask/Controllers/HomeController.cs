using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using TestTask.Models;
using TestTask.ViewModels;

namespace TestTask.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext dbContext;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        dbContext = new ApplicationDbContext();
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(IFormFile file)
    {
        GetUsersFromDb();
        string content;
        UserDto? userDto;

        if (file == null)
        {
            ViewBag.Message = $"File not uploaded";
            return View();
        }

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            content = reader.ReadToEnd();
        }

        try
        {
            userDto = JsonConvert.DeserializeObject<UserDto>(content);
        }
        catch (Exception ex)
        {
            ViewBag.Message = $"Inappropriate file content or wrong JSON configuration";
            return View();
        }
        var phoneEntity = dbContext.Phones.Add(new Phone
        {
            Mobile = userDto.Phones.Mobile,
            Home = userDto.Phones.Home,
        });
        dbContext.SaveChanges();

        var jobEntity = dbContext.Jobs.Add(new Job
        {
            Position = userDto.Job.Position,
            Experience = userDto.Job.Experience,
        });
        dbContext.SaveChanges();

        User user = new User
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            Age = userDto.Age,
            JobId = jobEntity.Entity.Id,
            Job = dbContext.Jobs.Find(jobEntity.Entity.Id),
            PhonesId = phoneEntity.Entity.Id,
            Phones = dbContext.Phones.Find(phoneEntity.Entity.Id),
        };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        ViewBag.Message = $"{file.FileName} is succesfully uploaded";
        return View(GetUsersFromDb());
    }

    private UsersViewModel GetUsersFromDb()
    {
        UsersViewModel usersViewModel = new UsersViewModel
        {
            Users = dbContext.Users.ToList()
        };
        foreach (var item in usersViewModel.Users)
        {
            item.Job = dbContext.Jobs.Find(item.JobId);
            item.Phones = dbContext.Phones.Find(item.PhonesId);
        }

        return usersViewModel;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}
