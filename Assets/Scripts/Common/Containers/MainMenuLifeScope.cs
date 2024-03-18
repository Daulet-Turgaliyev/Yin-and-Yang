using Common.Main_Menu;
using Common.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Common.Containers
{
    public class MainMenuLifeScope:LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<MainMenu>();
            
            base.Configure(builder);
        }
    }
}