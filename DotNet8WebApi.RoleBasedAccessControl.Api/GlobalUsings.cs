// Global using directives

global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using DotNet8WebApi.RoleBasedAccessControl.Api;
global using DotNet8WebApi.RoleBasedAccessControl.Api.Features.Auth;
global using DotNet8WebApi.RoleBasedAccessControl.Api.Middleware;
global using DotNet8WebApi.RoleBasedAccessControl.DbService.AppDbContexts;
global using DotNet8WebApi.RoleBasedAccessControl.Models.Enums;
global using DotNet8WebApi.RoleBasedAccessControl.Models.Features;
global using DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth;
global using DotNet8WebApi.RoleBasedAccessControl.Repositories.Features.Auth;
global using DotNet8WebApi.RoleBasedAccessControl.Shared;
global using DotNet8WebApi.RoleBasedAccessControl.Shared.Services.AuthService;
global using DotNet8WebApi.RoleBasedAccessControl.Shared.Services.SecurityServices;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using Newtonsoft.Json;