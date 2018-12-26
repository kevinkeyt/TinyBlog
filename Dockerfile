FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["TinyBlog.Web/TinyBlog.Web.csproj", "TinyBlog.Web/"]
COPY ["TinyBlog.Core/TinyBlog.Core.csproj", "TinyBlog.Core/"]
COPY ["TinyBlog.Infrastructure/TinyBlog.Infrastructure.csproj", "TinyBlog.Infrastructure/"]
RUN dotnet restore "TinyBlog.Web/TinyBlog.Web.csproj"
COPY . .
WORKDIR "/src/TinyBlog.Web"
RUN dotnet build "TinyBlog.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TinyBlog.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TinyBlog.Web.dll"]