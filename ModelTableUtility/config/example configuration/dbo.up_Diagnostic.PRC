SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

----------------------------------------------------------------------------------------------------------------------------------------
--
-- AUGUST 14, 2001
--
-- STORED PROCEDURE :   up_Diagnostics
--
-- This procedure sends given message to the Message Archiver
-- Also sends to event log.
--
----------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_Diagnostic
@sname     CHAR(20),
@pname     CHAR(20),
@diagLevel INT,
@diagMsg   CHAR(80),
@verbose   INT
AS
BEGIN
  IF ( @verbose = 1 )   PRINT @diagMsg
  IF ( @diagLevel = 0 )
  BEGIN
      INSERT DIAGLOG VALUES ( getdate(),  @sname, @pname, 'LOG', @diagMsg )
  END
   IF (@diagLevel = 1 )
   BEGIN
      INSERT DIAGLOG VALUES ( getdate(),  @sname, @pname, 'INFO', @diagMsg )
      EXEC  up_SendAlarm @diagLevel, @diagMsg
   END
   IF (@diagLevel = 2 )
   BEGIN
      INSERT DIAGLOG VALUES ( getdate(),  @sname, @pname, 'ERROR', @diagMsg )
      EXEC  up_SendAlarm @diagLevel, @diagMsg
   END
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

