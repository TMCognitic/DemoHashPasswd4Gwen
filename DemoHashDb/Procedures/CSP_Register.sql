CREATE PROCEDURE [dbo].[CSP_Register]
	@Email NVARCHAR(384),
	@Passwd BINARY(64)
AS
BEGIN
	BEGIN TRY		
		IF @Email IS NULL OR @Email NOT LIKE '%@%.%'
		BEGIN
			RAISERROR ('Invalid Email', 16, 1);
		END

		IF LEN(@Passwd) != 64
		BEGIN
			RAISERROR ('Invalid Passwd', 16, 1);
		END

		INSERT INTO Utilisateur (Email, Passwd) VALUES (@Email, dbo.CSF_HashPasswd(@Passwd));
		RETURN 0;
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE(),
				@ErrorSeverity INT = ERROR_SEVERITY(),
				@ErrorState INT = ERROR_STATE();

		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
		RETURN -1;
	END CATCH
END