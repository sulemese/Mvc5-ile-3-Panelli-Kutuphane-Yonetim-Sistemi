CREATE PROCEDURE BUGUNKU_EMANETLER
AS
BEGIN
SELECT TOP 1 COUNT(*) FROM TBL_DEPOSIT
WHERE TRANSACTIONSTATUS=0 AND convert(date,STARTTIME) = convert(date, getdate())
GROUP BY STARTTIME
ORDER BY COUNT(*) DESC
END

EXECUTE BUGUNKU_EMANETLER