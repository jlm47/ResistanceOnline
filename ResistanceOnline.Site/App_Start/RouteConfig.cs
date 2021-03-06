﻿using System.Web.Mvc;
using System.Web.Routing;

namespace ResistanceOnline.Site
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{action}/{gameid}/{playerguid}",
				defaults: new { controller = "Game", action = "Index", gameid = UrlParameter.Optional, playerguid = UrlParameter.Optional }
			);
		}
	}
}