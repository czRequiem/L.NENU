-- ================================================
--首页信息根据节目发布时间前五行节目信息展示
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,郑志鑫>
-- Create date: <Create Date,2016年4月22日 11:48:40,>
-- Description:	<Description,首页信息根据节目发布时间前五行节目信息展示,>
-- =============================================
CREATE PROCEDURE sp_ShowInfo_TheHost_BYTop5
AS
BEGIN
	--不输出受影响的行数
	SET NOCOUNT ON;

    --开始查询
	SELECT top 5 s.*,t.username from showInfo as s ,Show_TheHost_Info as st ,TheHostInfo as t
	where s.id = st.showinfoID and t.id = st.theHostinfoID
	order by s.createtime DESC
END
GO
