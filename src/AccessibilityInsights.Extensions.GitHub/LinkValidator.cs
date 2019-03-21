﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AccessibilityInsights.Extensions.GitHub
{
    public static class LinkValidator
    {
        private static readonly string GitHubLink = Properties.Resources.GitHubLink;
        private static readonly string AlphaNumricPattern = Properties.Resources.AlphaNumricPattern;

        public static bool IsValidGitHubRepoLink(string Link)
        {
            string UserNamePattern = string.Format(CultureInfo.InvariantCulture, Properties.Resources.UserNamePattern, AlphaNumricPattern);
            string RepoNamePattern = string.Format(CultureInfo.InvariantCulture, Properties.Resources.RepoNamePattern, AlphaNumricPattern);
            string LinkPatttern = string.Format(CultureInfo.InvariantCulture, Properties.Resources.LinkPatttern, GitHubLink, UserNamePattern, RepoNamePattern);
            Regex gitHubRepoLinRegex = new Regex(LinkPatttern, RegexOptions.IgnoreCase);
            if (!gitHubRepoLinRegex.IsMatch(Link))
            {
                return false;
            }

            string[] parts = Link.Replace(GitHubLink, "").Split('/');
            if (parts.Length != 2)
            {
                return false;
            }

            string userName = parts[0];
            string repoName = parts[1];
            if (!CheckUserNameMaxLength(userName) || !CheckRepoNameMaxLength(repoName) || !CheckRepoNameSpecialCases(repoName))
            {
                return false;
            }

            return true;
        }

        private static bool CheckUserNameMaxLength(string userName)
        {
            return (userName.Length <= 39 && userName.Length >= 1);
        }

        private static bool CheckRepoNameMaxLength(string repoName)
        {
            return (repoName.Length <= 100 && repoName.Length >= 1);
        }

        private static bool CheckRepoNameSpecialCases(string repoName)
        {
            if (repoName.Equals(Properties.Resources.RepoNameSpecialCasesDoubleDot, StringComparison.InvariantCulture))
            {
                return false;
            }

            return true;
        }

    }
}
