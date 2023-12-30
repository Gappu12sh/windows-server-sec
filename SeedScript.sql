MERGE INTO app.Lookup t
USING(
SELECT 'PARTY-EMAIL-OFFICIAL' as Code, 'PARTY-EMAIL-TYPE' As Type, 'Official' As Description, 1 As IsActive
UNION ALL
SELECT 'PARTY-EMAIL-PERSONAL', 'PARTY-EMAIL-TYPE', 'Personal', 1
UNION ALL
SELECT 'PARTY-PHONE-OFFICIAL', 'PARTY-PHONE-TYPE', 'Official', 1
UNION ALL
SELECT 'PARTY-PHONE-PERSONAL', 'PARTY-PHONE-TYPE', 'Personal', 1
UNION ALL
SELECT 'PARTY-ADDRESS-OFFICIAL', 'PARTY-ADDRESS-TYPE', 'Official', 1
UNION ALL
SELECT 'PARTY-ADDRESS-PERSONAL', 'PARTY-ADDRESS-TYPE', 'Personal', 1
UNION ALL
SELECT 'CITY-GURGAON', 'CITY-TYPE', 'Gurgaon', 1
UNION ALL
SELECT 'CITY-NOIDA', 'CITY-TYPE', 'Noida', 1
UNION ALL
SELECT 'STATE-HARYANA', 'STATE-TYPE', 'Haryana', 1
UNION ALL
SELECT 'STATE-UP', 'STATE-TYPE', 'Uttar Pradesh', 1
UNION ALL
SELECT 'COUNTRY-INDIA', 'COUNTRY-TYPE', 'India', 1
UNION ALL
SELECT 'UNIT-KILOGRAM', 'UNIT-TYPE', 'Kg', 1
UNION ALL
SELECT 'UNIT-GRAMS', 'UNIT-TYPE', 'Grams', 1
UNION ALL
SELECT 'TERMS-CONDITION', 'TERMS', 'G.S.T. @ 18 % Extra.  Rates are valid only for 01 Month.', 1
) s
ON (t.Code = s.Code and t.Type = s.Type)
WHEN MATCHED THEN
    UPDATE SET t.Code = s.Code, t.Type = s.Type, t.Description = s.Description, t.IsActive = s.IsActive
WHEN NOT MATCHED THEN
    INSERT (Code, Type, Description, IsActive)
    VALUES (s.Code, s.Type, s.Description, s.IsActive);

      MERGE INTO app.Modules t
   USING(
   SELECT 'DASHBOARD' as ModuleCode,'Dashboard' as ModuleName,'Static' as ModuleUrl, 1 As IsActive
   UNION ALL
   SELECT 'DEPARTMENT' as ModuleCode,'Department','Static' as ModuleUrl, 1
   UNION ALL
   SELECT 'APPLICATION-USAGE' as ModuleCode,'ApplicationUsage','Static' as ModuleUrl, 1
   UNION ALL
   SELECT 'REPRESENTATIVES' as ModuleCode,'Representatives','Static' as ModuleUrl, 1
   UNION ALL
   SELECT 'PRODUCT' as ModuleCode,'Product','Static' as ModuleUrl, 1
   UNION ALL
   SELECT 'CONTACT' as ModuleCode,'Contact','Static' as ModuleUrl, 1
   UNION ALL
   SELECT 'MATERIAL' as ModuleCode,'Material','Static' as ModuleUrl, 1
   UNION ALL
   SELECT 'SAMPLE' as ModuleCode,'Samples','Static' as ModuleUrl, 1
   UNION ALL
   SELECT 'USER-DETAILS' as ModuleCode,'UserDetails','Static' as ModuleUrl, 1
   UNION ALL
   SELECT 'USER-MODULE-PERMISSION' as ModuleCode,'UserModulePermission','Static' as ModuleUrl, 1
    UNION ALL
   SELECT 'CUSTOMER-REPORT' as ModuleCode,'CustomerReport','Static' as ModuleUrl, 1
   UNION ALL
   SELECT 'QUOTATIONS' as ModuleCode,'Quotations','Static' as ModuleUrl, 1
   UNION ALL
   SELECT 'PARTY-ADDRESS' as ModuleCode,'PartyAddress','Static' as ModuleUrl, 1
   )s
   ON (t.ModuleCode = s.ModuleCode)
WHEN MATCHED THEN
    UPDATE SET t.ModuleCode=s.ModuleCode, t.ModuleName = s.ModuleName, t.IsActive = s.IsActive
WHEN NOT MATCHED THEN
    INSERT (ModuleCode,ModuleName, ModuleUrl, IsActive)
    VALUES (s.ModuleCode,s.ModuleName,s.ModuleUrl, s.IsActive);

    MERGE INTO app.Lookup t
USING(
SELECT 'MODULE-ACTION-ADD' as Code, 'MODULE-ACTION-TYPE' As Type, 'Add' As Description, 1 As IsActive
UNION ALL
SELECT 'MODULE-ACTION-EDIT', 'MODULE-ACTION-TYPE', 'Edit', 1
UNION ALL
SELECT 'MODULE-ACTION-DELETE', 'MODULE-ACTION-TYPE', 'Delete', 1
UNION ALL
SELECT 'MODULE-ACTION-VIEW', 'MODULE-ACTION-TYPE', 'View', 1
UNION ALL
SELECT 'MODULE-ACTION-ADD-ADDRESS', 'MODULE-ACTION-TYPE', 'Add Address', 1
UNION ALL
SELECT 'MODULE-ACTION-ADD-RATE', 'MODULE-ACTION-TYPE', 'Add Rate', 1
UNION ALL
SELECT 'MODULE-ACTION-VIEW-PARTIES', 'MODULE-ACTION-TYPE', 'View Party', 1
) s
ON (t.Code = s.Code and t.Type = s.Type)
WHEN MATCHED THEN
    UPDATE SET t.Code = s.Code, t.Type = s.Type, t.Description = s.Description, t.IsActive = s.IsActive
WHEN NOT MATCHED THEN
    INSERT (Code, Type, Description, IsActive)
    VALUES (s.Code, s.Type, s.Description, s.IsActive);

     MERGE INTO app.ModuleActions t
USING(
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='DASHBOARD') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='DEPARTMENT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='DEPARTMENT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-EDIT') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='DEPARTMENT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-DELETE') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='DEPARTMENT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-VIEW') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='APPLICATION-USAGE') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='APPLICATION-USAGE') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-EDIT') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='APPLICATION-USAGE') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-DELETE') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='APPLICATION-USAGE') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-VIEW') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='REPRESENTATIVES') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='REPRESENTATIVES') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-EDIT') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='REPRESENTATIVES') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-DELETE') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='REPRESENTATIVES') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-VIEW') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='REPRESENTATIVES') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-VIEW-PARTIES') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='PRODUCT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='PRODUCT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-EDIT') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='PRODUCT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-DELETE') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='PRODUCT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-VIEW') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='USER-DETAILS') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='USER-DETAILS') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-EDIT') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='USER-DETAILS') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-DELETE') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='USER-DETAILS') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-VIEW') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='USER-MODULE-PERMISSION') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='USER-MODULE-PERMISSION') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-EDIT') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='USER-MODULE-PERMISSION') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-DELETE') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='USER-MODULE-PERMISSION') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-VIEW') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='CONTACT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='CONTACT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-EDIT') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='CONTACT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-DELETE') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='CONTACT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-VIEW') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='CONTACT') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD-ADDRESS') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='MATERIAL') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='MATERIAL') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-EDIT') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='MATERIAL') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-DELETE') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='MATERIAL') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-VIEW') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='MATERIAL') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD-RATE') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='SAMPLE') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='SAMPLE') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-EDIT') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='SAMPLE') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-DELETE') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='SAMPLE') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-VIEW') as ActionId
UNION ALL
SELECT (SELECT ModuleId FROM app.Modules WHERE ModuleCode='SAMPLE') as ModuleId, (SELECT LookupId FROM app.Lookup WHERE Code='MODULE-ACTION-ADD-RATE') as ActionId
) s
ON (t.ModuleId = s.ModuleId and t.ActionId = s.ActionId)
WHEN NOT MATCHED THEN
    INSERT (ModuleId, ActionId)
    VALUES (s.ModuleId, s.ActionId);