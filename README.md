## Criar novo projeto
```
dotnet new web -o MeuTodo
```

## Incluir packages
```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Swashbuckle.AspNetCore
```

## Criar banco de dados
```
dotnet ef migrations add Initial
dotnet ef database update
```

## Executar
```
dotnet watch run
```

## Publicar no heroku
```
git init
heroku git:remote -a meu-todo
heroku buildpacks:set https://github.com/jincod/dotnetcore-buildpack
git add .
git commit -am "make it better"
git push heroku master
```

## Exemplo de url (swagger)
https://localhost:5001/swagger/index.html