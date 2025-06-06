﻿namespace MuseumService;

using Microsoft.EntityFrameworkCore;
using Models;
using Models.Services;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        await context.Database.MigrateAsync();

        await InitializeEmployees(context);
        await InitializeGames(context);
        await InitializeScores(context);
    }

    private static async Task InitializeEmployees(AppDbContext context)
    {
        if (!await context.Employees.AnyAsync())
        {
            var admin = new Employee
            {
                Username = "admin",
                PasswordHash = AuthService.HashPassword("password123"),
                FullName = "Administrator",
                IsActive = true
            };

            var guide = new Employee
            {
                Username = "guide",
                PasswordHash = AuthService.HashPassword("password123"),
                FullName = "Museum Guide",
                IsActive = true
            };

            context.Employees.AddRange(admin, guide);
            await context.SaveChangesAsync();
        }
    }

    private static async Task InitializeGames(AppDbContext context)
    {
        if (!await context.Games.AnyAsync())
        {
            context.Games.AddRange(
                new Game
                {
                    GameName = "Art Quiz",
                    Description = "Тест знаний по искусству",
                    GameType = "quiz",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Game
                {
                    GameName = "Treasure Hunt",
                    Description = "Игра по поиску сокровищ в музее",
                    GameType = "game",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Game
                {
                    GameName = "History Timeline",
                    Description = "Расставьте исторические события в правильном порядке",
                    GameType = "quiz",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            );
            
            await context.SaveChangesAsync();
        }
    }

    private static async Task InitializeScores(AppDbContext context)
    {
        if (!await context.Scores.AnyAsync())
        {
            var games = await context.Games.ToListAsync();
            if (!games.Any()) 
                return;

            var artQuiz = games.FirstOrDefault(g => g.GameName == "Art Quiz");
            var treasureHunt = games.FirstOrDefault(g => g.GameName == "Treasure Hunt");
            var historyTimeline = games.FirstOrDefault(g => g.GameName == "History Timeline");

            if (artQuiz == null || treasureHunt == null || historyTimeline == null)
                return;

            context.Scores.AddRange(
                new Score
                {
                    GameId = artQuiz.GameId,
                    PlayerName = "Иван",
                    ScoreValue = 85,
                    PlayedAt = DateTime.Parse("2024-02-01 15:20:00")
                },
                new Score
                {
                    GameId = artQuiz.GameId,
                    PlayerName = "Мария",
                    ScoreValue = 92,
                    PlayedAt = DateTime.Parse("2024-02-02 11:45:00")
                },
                new Score
                {
                    GameId = treasureHunt.GameId,
                    PlayerName = "Алексей",
                    ScoreValue = 75,
                    PlayedAt = DateTime.Parse("2024-02-03 14:10:00")
                },
                new Score
                {
                    GameId = historyTimeline.GameId,
                    PlayerName = "Елена",
                    ScoreValue = 88,
                    PlayedAt = DateTime.Parse("2024-02-04 16:30:00")
                }
            );

            await context.SaveChangesAsync();
        }
    }
}