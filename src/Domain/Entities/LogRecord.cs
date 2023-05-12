using System;
using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Domain.Entities;

public class LogRecord
{
    LogRecord() { }

    public long Id { get; private set; }
    public string ShortMessage { get; private set; } = string.Empty;
    public LogLevelType LevelId { get; private set; } = LogLevelType.Information;
    public PresentationAssembly PresentationId { get; set; }
    public string? FullMessage { get; private set; }
    public string? IpAddress { get; private set; }
    public string? Url { get; private set; }
    public string? ReferrerUrl { get; private set; }
    public Guid? UserId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    #region Methods
    /// <summary>
    /// Create log instance.
    /// </summary>
    /// <param name="shortMessage">Short message for log.</param>
    /// <param name="levelId">Log level id.</param>
    /// <param name="fullMessage">Full message, Inner exeption message log.</param>
    public LogRecord Create(string shortMessage, LogLevelType levelId = LogLevelType.Information, string? fullMessage = null)
    {
        ShortMessage = shortMessage;
        LevelId = levelId;
        if (fullMessage is not null)
            FullMessage = fullMessage;
        CreatedAt = DateTime.Now;
        return this;
    }

    /// <summary>
    /// Add Current System IP address from request.
    /// </summary>
    /// <param name="ipAddress">Current system ip address.</param>
    public LogRecord WithIpAddress(string ipAddress)
    {
        IpAddress = ipAddress;
        return this;
    }

    /// <summary>
    /// Add user id.
    /// </summary>
    /// <param name="userId">Current user id. (Guid)</param>
    public LogRecord WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }

    /// <summary>
    /// Add request url.
    /// </summary>
    /// <param name="url">Current request URL.</param>
    public LogRecord WithUrl(string url)
    {
        Url = url;
        return this;
    }

    /// <summary>
    /// Add referral url from request header.
    /// </summary>
    /// <param name="referrerUrl">Current request referral url.</param>
    public LogRecord WithReferralUrl(string referrerUrl)
    {
        ReferrerUrl = referrerUrl;
        return this;
    }
    #endregion

    #region RelationShip
    public virtual User? User { get; set; }
    #endregion
}