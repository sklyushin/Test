using HockeyEventService.APIs.Common;
using HockeyEventService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace HockeyEventService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class PlaceFindManyArgs : FindManyInput<Place, PlaceWhereInput> { }
