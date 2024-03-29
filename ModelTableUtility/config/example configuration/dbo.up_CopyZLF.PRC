SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


---------------------------------------------------------------------------------------
-- STORED PROCEDURE
--
-- NAME:         up_CopyZLF
-- DATABASE:     Model Database
-- PURPOSE:      To copy family and grade setup data from Model Tables Utility.
-- AUTHOR:       Kiran Kumar Mallipudi,  GE Toshiba Automation Systems
-- ASSUMPTIONS: 
-- EFFECTS:      Z_LF table, Updates or Insert data for destination family by copying 
--               from source family
--               
-- INPUT PARAMS: 
--    srcFamily     required - source family
--    destGrade     required - destnation family
--    sname         optional - calling screen name for diaglog; defaults to ANONYMOUS 
--    verbose       optional - if 1, enables PRINT messages to Query Analyzer.
--
-- OTHER INPUTS: none
--
-- RETURNS:      Failure or Suceess
-- CREATED:      September 12, 2003
-- CHANGES:      Original
--
-------------------------------------------------------------------------------------------

CREATE PROCEDURE up_CopyZLF
@srcFamily      CHAR(20),
@destFamily     CHAR(20), 
@sname          CHAR (20)  = 'ANONYMOUS',  -- calling screen name - for diagnostic log
@verbose        INT        = 0,            -- output messages to Query Analyzer output window
@status         CHAR(80)   = '_' OUTPUT           -- result status returned to caller

AS

BEGIN
    -- set up for diagnostics 
    DECLARE @pname       CHAR(20) 
    DECLARE @diagLvl     INT
    DECLARE @lvl_LOGONLY INT
    DECLARE @SUCCESS     INT
    DECLARE @FAILURE    INT
    DECLARE @diagMsg     CHAR(80)
    DECLARE @msgPrefix   CHAR(20)
    DECLARE @returnStat  INT

    

    SELECT  @pname       = 'up_CopyZLF'
    SELECT  @lvl_LOGONLY = 0  
    SELECT  @SUCCESS     = 0  
    SELECT  @FAILURE     = 1  
    SELECT  @msgPrefix   = ''
    SELECT  @diagLvl     = @lvl_LOGONLY  -- initial value
    SELECT  @returnStat  = @FAILURE
 
    IF ( @verbose = 0 ) SET nocount ON
    
    -- Check for Source Family is available or not in main table and in sub tables
    IF NOT EXISTS ( SELECT sgf FROM Z_LF WHERE sgf = @srcFamily) or NOT EXISTS ( SELECT sgf FROM Z_LF_2d where sgf = @srcFamily)
    BEGIN
        SELECT @status = 'Source Family is not available in Z_LF'
        SELECT @diagMsg = @msgPrefix + ' ' + @status
        EXEC up_Diagnostic @sname, @pname, @lvl_LOGONLY, @diagMsg, @verbose  
        IF ( @verbose = 1 ) print @diagMsg  
    END
    
    ELSE

    BEGIN
         -- Copying Z_LF table

         -- select complete data of Source Family into a temperory table
         SELECT * INTO #t_ModelTemp FROM Z_LF WHERE sgf = @srcFamily

         -- Update temperory table by changing sgf name to destination family
         UPDATE #t_ModelTemp SET sgf = @destFamily

         -- Delete complete existing data of destnation family
         -- If destination family is new, it will not delete any records.
         DELETE Z_LF WHERE sgf =@destFamily

         -- Insert temperory table data 
         INSERT Z_LF SELECT * FROM #t_ModelTemp
        
  
        -- Copying Z_LF_2D table

        -- select complete data of Source Family into a temperory table
        SELECT * INTO #t_ModelTemp2D FROM Z_LF_2d WHERE sgf = @srcFamily

        -- Update temperory table by changing sgf name to destination family
        UPDATE #t_ModelTemp2D SET sgf = @destFamily

        -- Delete complete existing data of destnation family
        -- If destination family is new, it will not delete any records.
        DELETE Z_LF_2d WHERE sgf =@destFamily

        -- Insert temperory table data 
        INSERT Z_LF_2d SELECT * FROM #t_ModelTemp2D


        ------------------------------------------------------
        -- Deliver diagnostics
        SELECT @status = 'Copied Source Family data into Destnation family in Z_LF '
        SELECT @diagMsg = @msgPrefix + ' ' + @status
        EXEC up_Diagnostic @sname, @pname, @lvl_LOGONLY, @diagMsg, @verbose  
        IF ( @verbose = 1 ) print @diagMsg
        SELECT @returnStat = @SUCCESS

    END
    
    RETURN @returnStat
	
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

