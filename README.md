## ef-core-issue-32244
Sample solution demonstrating [EF Core issue 32244](https://github.com/dotnet/efcore/issues/32244).

The sample uses PostgreSQL on localhost using `postgres:pgpwd` as the username and password, change in DbContext.cs. 
Tests create and drop the DB as needed.

Run:
```sh
dotnet test --logger:"console;verbosity=detailed"`
```

Notice that the `TestWithLoggingCacheDisabled` has 3 `info` log items after the `error` log item, while 
`TestWithLoggingCacheEnabled` doesn't include any `info` lines after the `error` line.

Also included is the same test hosted in a console app. When the console app is run from an IDE such as Jetbrains Rider,
the 3 `info` log items are included after the `error` item. When the console app is run from the command line, the 
3 `info` log items **are not** included:
```sh
dotnet run --project EfCoreIssue32244.ConsoleRunner/EfCoreIssue32244.ConsoleRunner.csproj
```

See the issue link for more details.
