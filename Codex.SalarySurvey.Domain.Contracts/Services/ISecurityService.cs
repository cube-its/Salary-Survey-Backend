﻿using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Domain.Contracts.Services
{
    /// <summary>
    /// Security service interface that exposes the relevant operations.
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Gets the authenticated user from the http context by the access token.
        /// </summary>
        /// <returns>Authenticated user</returns>
        User GetAuthenticatedUser();
    }
}
