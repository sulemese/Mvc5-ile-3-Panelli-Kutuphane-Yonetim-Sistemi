CREATE PROCEDURE ENBASARILIPERSONEL
AS
BEGIN
 SELECT EMPLOYEEID,COUNT(*) FROM TBL_DEPOSIT
 GROUP BY EMPLOYEEID
 ORDER BY COUNT(*)
 END

