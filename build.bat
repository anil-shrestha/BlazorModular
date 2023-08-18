dotnet build -c Release Modular.Library
dotnet build -c Release Modular.Shared
dotnet build -c Release Modular.HrModule
dotnet build -c Release Modular.FinanceModule
copy Modular.FinanceModule\bin\Release\net6.0\Modular.FinanceModule.dll MainWeb\Modules\ /y
copy Modular.HrModule\bin\Release\net6.0\Modular.HrModule.dll MainWeb\Modules\ /y
dotnet build -c Release MainWeb