﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiResource> Apis =>
            new[]
            {
                new ApiResource("scriboapi","Scirob API") 
            };
        
        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "ScriboId",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("very secret".Sha512())
                    },
                    AllowedScopes = {"scriboapi"}
                } 
            };
        
    }
}