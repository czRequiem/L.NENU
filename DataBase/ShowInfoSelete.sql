-- =============================================
-- Author:		<郑志鑫>
-- Create date: <2015-11-3 16:33:44>
-- Description:	< exec sp_VReserveTrainInfo_GetVReserveTrainInfo_By @ClassNo = '1400165' , @StudentID = '140016528' ,@BeginTime = '2015-12-06' ,@EndTime  = '2015-12-8' >
-- =============================================
alter PROCEDURE [dbo].[sp_ShowInfo_ShowInfoSelete_By]
	@ID int,
	@ShowTitle nvarchar(100),
	@CreateTime datetime,
	@Intro nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @SQL nvarchar(400) ;
	declare @where nvarchar(400) ;

	--初始化变量
	set @SQL = 'select * from ShowInfo';
	set @where = '';

	--组织查询条件
	if @ID != ''
		set @where ='@ID= '''+@ID+'''';
		
	if @ShowTitle != ''
	Begin
		if @where!= ''
			set @where = @where + ' and '
		set @where = @where + 'ShowTitle='''+@ShowTitle+'''';
	End
	if @ShowTitle != ''
	Begin
		if @where!= ''
			set @where = @where + ' and '
		set @where = @where + 'ShowTitle='''+@Intro+'''';
	End
	if @CreateTime != ''
	Begin
		if @where!= ''
			set @where = @where + ' and '
		set @where = @where + 'CreateTime < '''+@CreateTime+''''
	End

	if @where != ''
		set @SQL = @SQL + ' where ' + @where

	exec (@SQL)
END