### For add new EF migration use 
        
        dotnet ef migrations add "initial-migration" --startup-project ../FormulaOne.Api
do it from FormulaOne.DataService project


### To apply migrations use

        dotnet ef database update --startup-project ../FormulaOne.Api
