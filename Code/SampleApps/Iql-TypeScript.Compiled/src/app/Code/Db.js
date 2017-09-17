import { Types } from "../Iql/TsUtility/Types";
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
//# sourceMappingURL=C:/Users/josh-xps/AppData/Local/Temp/f21280a4-0f82-4c19-a037-070e78937fa7/Input/Code/Db.js.map