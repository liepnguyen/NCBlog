using Microsoft.AspNetCore.Authorization;
using Planru.CrossCutting.Identity.Core;
using Planru.CrossCutting.Identity.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Planru.CrossCutting.Identity.Web.Policies
{
    public class ViewUserByIdRequirement : IAuthorizationRequirement
    {

    }


    public class ViewUserByIdHandler : AuthorizationHandler<ViewUserByIdRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewUserByIdRequirement requirement, string targetUserId)
        {
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ViewUsers) || GetIsSameUser(context.User, targetUserId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }


        private bool GetIsSameUser(ClaimsPrincipal user, string targetUserId)
        {
            return Utilities.GetUserId(user) == targetUserId;
        }
    }
}
