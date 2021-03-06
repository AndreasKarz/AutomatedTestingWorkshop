﻿using FunkyBDD.SxS.Selenium.APOM;
using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;

namespace FunkyBDD.SxS.Framework.APOM.Atoms
{
    public class LinkButton : DefaultProps
    {
        public LinkButton(IWebElement parent, By by)
        {
            Component = parent.FindElementFirstOrDefault(by);
        }

        public string Text => Component.Text;
    }
}
