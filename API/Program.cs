using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);//First of all, We use this builder config and this is going to go ahead and create our web application instance effectively which allows us to run our application.
//When we run net run it comes and takes a look inside this file and executes the code inside here.
//Anything before app builder.build is considered our services container. 

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();
//Add middleware before reqeust hits to controller

// // Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();//Authentication part just asks, Do you have a valid token?
app.UseAuthorization();//Authorization part says, okay, you have a valid token now what are you allowed to do?.

app.MapControllers();

app.Run();
