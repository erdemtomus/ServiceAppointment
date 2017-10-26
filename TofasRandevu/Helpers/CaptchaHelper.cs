using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TofasRandevu.Helpers
{
    public static class CaptchaHelper
    {
        public static MvcCaptcha GetExampleCaptcha(int Width, int Height)
        {
            MvcCaptcha exampleCaptcha = GetExampleCaptcha();
            exampleCaptcha.ImageSize = new System.Drawing.Size(Width, Height);
            return exampleCaptcha;
        }

        public static MvcCaptcha GetExampleCaptcha()
        {
            MvcCaptcha exampleCaptcha = new MvcCaptcha("ExampleCaptcha");
            exampleCaptcha.ImageStyle = BotDetect.ImageStyle.BlackOverlap;
            exampleCaptcha.SoundEnabled = false;
            exampleCaptcha.UserInputID = "CaptchaCode";
            return exampleCaptcha;
        }
    }

}