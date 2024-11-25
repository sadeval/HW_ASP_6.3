namespace BattleSimulation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавление сервисов в контейнер.
            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            var app = builder.Build();

            // Настройка конвейера обработки HTTP-запросов.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Battle/Error"); 
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Battle}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
