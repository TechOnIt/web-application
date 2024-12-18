﻿using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Application.Services.AssemblyServices;

namespace TechOnIt.Application.Events.IdentityNotifications.Authentication;

public class SignUpUserNotifications : INotification
{
    public const string defaultEmailTemp = "<div dir='ltr' style='font-family:tahoma;font-size:14px'>Welcome To Iot Panel</div>";

    public SignUpUserNotifications(string phonenumber, string email)
    {
        Phonenumber = phonenumber;
        Email = email;
        Subject = "TechOnIt.Co";
        Message = defaultEmailTemp;
    }

    public SignUpUserNotifications(string phonenumber, string email, string subject, string message)
    {
        Phonenumber = phonenumber;
        Email = email;
        Subject = subject;
        Message = message;
    }

    public SignUpUserNotifications()
    {
        Subject = "TechOnIt.Co";
        Message = defaultEmailTemp;
    }

    public string Phonenumber { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}

public class SignUpUserSmsNotificationsHandler : INotificationHandler<SignUpUserNotifications>
{
    public async Task Handle(SignUpUserNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}

public class SignUpUserEmailNotificationsHandler : INotificationHandler<SignUpUserNotifications>
{
    #region constructor
    private readonly ISmtpEmailSender _smtpEmailService;
    private readonly IAppSettingsService<AppSettingDto> _appSettingsService;
    private readonly IUnitOfWorks _unitOfWorks;
    public SignUpUserEmailNotificationsHandler(ISmtpEmailSender smtpEmailService, IAppSettingsService<AppSettingDto> appSettingsService, IUnitOfWorks unitOfWorks)
    {
        _smtpEmailService = smtpEmailService;
        _appSettingsService = appSettingsService;
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task Handle(SignUpUserNotifications notification, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(notification.Email))
            notification.Email = await _unitOfWorks.UserRepository.GetEmailByPhoneNumberAsync(notification.Phonenumber);

        await _smtpEmailService.SendEmailAsync(_appSettingsService.GetGmailSenderAddress(), notification.Email, notification.Subject, notification.Message,
            _appSettingsService.GetEmailSettings());

        await Task.CompletedTask;
    }
}

public class SignUpUserNotificationsValidations : BaseFluentValidator<SignUpUserNotifications>
{
    public SignUpUserNotificationsValidations()
    {
        RuleFor(a => a.Phonenumber)
            .NotNull()
            .NotEmpty()
            .MinimumLength(11)
            .MaximumLength(11)
            ;
    }
}