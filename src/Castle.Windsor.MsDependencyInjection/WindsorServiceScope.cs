﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace Castle.Windsor.MsDependencyInjection
{
    /// <summary>
    /// Implements <see cref="IServiceScope"/>.
    /// </summary> 
    public class WindsorServiceScope : IServiceScope
    {
        public IServiceProvider ServiceProvider { get; }

        public MsLifetimeScope LifetimeScope { get; }

        public WindsorServiceScope(IWindsorContainer container, MsLifetimeScope currentMsLifetimeScope)
        {
            LifetimeScope = new MsLifetimeScope();

            if (currentMsLifetimeScope != null)
            {
                currentMsLifetimeScope.Children.Add(LifetimeScope);
            }

            using (MsLifetimeScope.Using(LifetimeScope))
            {
                ServiceProvider = container.Resolve<IServiceProvider>();
            }
        }
         
        public void Dispose()
        {
            LifetimeScope.Dispose();
        }
    }
}