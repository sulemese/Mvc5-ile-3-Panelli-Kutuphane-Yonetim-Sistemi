CREATE TRIGGER KITAPDURUM
ON TBL_DEPOSIT
AFTER INSERT
AS
DECLARE @KITAP INT
SELECT @KITAP=BOOKID FROM inserted --hareket tablomuza eklenen kay�ttaki BOOKID de�erini @K�TAP DEGER�NE ATADIK.
UPDATE TBL_BOOK SET STATUS=0 WHERE ID=@KITAP