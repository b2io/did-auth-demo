using DidAuthDemo.Issuer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


//{
//    "type": "object",
//  "properties": {
//        "Name": {
//            "type": "string"
//        },
//    "AlternateName": {
//            "type": [
//              "string",
//        "null"
//      ]
//    },
//    "Description": {
//            "type": "string"
//    },
//    "Logo": {
//            "type": [
//              "string",
//        "null"
//      ]
//    },
//    "Urls": {
//            "type": [
//              "array",
//        "null"
//      ],
//      "items": {
//                "type": [
//                  "string",
//          "null"
//        ]
//      }
//        },
//    "ManagerDids": {
//            "type": "array",
//      "items": {
//                "type": [
//                  "string",
//          "null"
//        ]
//      }
//        },
//    "foundingDate": {
//            "type": "string"
//    },
//    "Quorum": {
//            "type": "string"
//    },
//    "PassRate": {
//            "type": "string"
//    },
//    "VotingDays": {
//            "type": "string"
//    },
//    "Vkey": {
//            "type": "string"
//    },
//    "Skey": {
//            "type": "string"
//    },
//    "Did": {
//            "type": "string"
//    }
//    },
//  "required": [
//    "Name",
//    "AlternateName",
//    "Description",
//    "Logo",
//    "Urls",
//    "ManagerDids",
//    "foundingDate",
//    "Quorum",
//    "PassRate",
//    "VotingDays",
//    "Vkey",
//    "Skey",
//    "Did"
//  ]
//}