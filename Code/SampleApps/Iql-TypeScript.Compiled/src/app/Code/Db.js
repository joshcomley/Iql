import { Types } from "../TsUtility/Types";
var Db = /** @class */ (function () {
    function Db() {
        if (!Db.Initialized) {
            Db.Initialized = true;
            Db.People = new Array();
            Db.People.push((function () {
                var obj = Types.New_TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person();
                obj.Age = 32;
                obj.Id = 2;
                obj.Name = "Cara";
                ;
                return obj;
            })());
            Db.People.push((function () {
                var obj = Types.New_TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person();
                obj.Age = 24;
                obj.Id = 1;
                obj.Name = "Paulina";
                ;
                return obj;
            })());
            Db.People.push((function () {
                var obj = Types.New_TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person();
                obj.Age = 31;
                obj.Id = 3;
                obj.Name = "Kiera";
                ;
                return obj;
            })());
        }
    }
    Db.Initialized = false;
    return Db;
}());
export { Db };
//# sourceMappingURL=C:/Users/joshc/AppData/Local/Temp/6d213cd9-a86b-465e-a6a2-d613e08a3feb/Input/Code/Db.js.map