﻿using System;
using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Navtrack.Library.DI;

namespace Navtrack.Web.Services.LetsEncrypt
{
    [Service(typeof(IServerCertificateSelector), ServiceLifetime.Singleton)]
    internal sealed class ServerCertificateSelector : IServerCertificateSelector
    {
        private readonly ConcurrentDictionary<string, X509Certificate2> certificates =
            new ConcurrentDictionary<string, X509Certificate2>(StringComparer.OrdinalIgnoreCase);

        public void Add(X509Certificate2 certificate)
        {
            string commonName = certificate.GetNameInfo(X509NameType.SimpleName, false);

            certificates.AddOrUpdate(commonName, certificate, (x, y) => certificate);
        }

        public X509Certificate2 Select(ConnectionContext features, string domainName)
        {
            certificates.TryGetValue(domainName, out X509Certificate2 certificate);

            return certificate;
        }
    }
}