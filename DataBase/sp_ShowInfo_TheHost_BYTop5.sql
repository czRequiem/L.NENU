-- ================================================
--��ҳ��Ϣ���ݽ�Ŀ����ʱ��ǰ���н�Ŀ��Ϣչʾ
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,֣־��>
-- Create date: <Create Date,2016��4��22�� 11:48:40,>
-- Description:	<Description,��ҳ��Ϣ���ݽ�Ŀ����ʱ��ǰ���н�Ŀ��Ϣչʾ,>
-- =============================================
CREATE PROCEDURE sp_ShowInfo_TheHost_BYTop5
AS
BEGIN
	--�������Ӱ�������
	SET NOCOUNT ON;

    --��ʼ��ѯ
	SELECT top 5 s.*,t.username from showInfo as s ,Show_TheHost_Info as st ,TheHostInfo as t
	where s.id = st.showinfoID and t.id = st.theHostinfoID
	order by s.createtime DESC
END
GO
