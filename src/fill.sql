insert into user_(login,
password_,
name_,
surname)
values ('Inlucker', 'qwerty', 'Arseny', 'Pronin');

insert into company(companyid,
title,
foundationyear)
values (1, 'tilt', 1994);

insert into department(departmentid,
title,
company,
foundationyear,
activityfield)
values (1, 'Testing', 1, 1994, 'Information tech');

insert into employee(employeeid,
user_,
company,
department,
permission_)
values (1, 'Inlucker', 1, null, 2);

insert into objective(objectiveid,
parentobjective,
company,
department,
title,
termbegin,
termend,
estimatedtime)
values (1, null, 1, null, 'heh', '1994-04-09', '2014-06-25', '18 days 06:40:56');
