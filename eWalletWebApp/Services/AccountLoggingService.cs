using System;
using System.Threading;
using System.Threading.Tasks;
using eWalletWebApp.EfCore;
using eWalletWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace eWalletWebApp.Services;

public class AccountLoggingService : IHostedService, IDisposable {
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer _timer;

    public AccountLoggingService(IServiceScopeFactory scopeFactory) {
        this._scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        Log.Information("Starting Account Logging Service");
        _timer = new Timer(LogAccounts, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private void LogAccounts(object state) {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EWalletContext>();
        var accounts = context.Accounts.Where(a => a.Balance > 0).ToList();
        foreach (var account in accounts) {
            Log.Information($"Account {account.AccountId} Balance: {account.Balance}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        Log.Information("Stopping Account Logging Service");
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose() {
        _timer?.Dispose();
    }
}