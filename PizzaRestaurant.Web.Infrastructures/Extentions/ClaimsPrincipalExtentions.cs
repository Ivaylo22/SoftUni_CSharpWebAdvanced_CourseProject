﻿namespace PizzaRestaurant.Web.Infrastructures.Extentions
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Security.Claims;

    using static PizzaRestaurant.Common.GeneralApplicationConstants;
    public static class ClaimsPrincipalExtentions
    {
        public static string? GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}
