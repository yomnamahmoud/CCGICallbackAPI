USE [MazadyComviva]
GO
/****** Object:  Table [dbo].[CallbackCCG]    Script Date: 06/20/2017 15:17:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CallbackCCG](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MSISDN] [nvarchar](50) NULL,
	[Result] [nvarchar](50) NULL,
	[Reason] [nvarchar](50) NULL,
	[productId] [nvarchar](50) NULL,
	[transID] [nvarchar](50) NULL,
	[TPCGID] [nvarchar](50) NULL,
	[Songname] [nvarchar](50) NULL,
 CONSTRAINT [PK_CallbackCCG] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_CallBackCCGInsert]    Script Date: 06/20/2017 15:17:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_CallBackCCGInsert]
	-- Add the parameters for the stored procedure here
	@MSISDN nvarchar(50),
    @Result nvarchar(50),
     @Reason nvarchar(50),
           @productId nvarchar(50),
           @transID nvarchar(50),
           @TPCGID nvarchar(50),
           @Songname nvarchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


INSERT INTO [dbo].[CallbackCCG]
           ([MSISDN]
           ,[Result]
           ,[Reason]
           ,[productId]
           ,[transID]
           ,[TPCGID]
           ,[Songname])
     VALUES(
           @MSISDN,
    @Result ,
     @Reason ,
           @productId ,
           @transID ,
           @TPCGID ,
           @Songname);

 
SELECT [ID]
      ,[MSISDN]
      ,[Result]
      ,[Reason]
      ,[productId]
      ,[transID]
      ,[TPCGID]
      ,[Songname]
  FROM [dbo].[CallbackCCG]

      WHERE  [ID] = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[usp_CallbackCCG_Query]    Script Date: 06/20/2017 15:17:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_CallbackCCG_Query]
as
begin
	select top 100 * from CallbackCCG where TPCGID is not null;
end
GO
/****** Object:  StoredProcedure [dbo].[usp_CallbackCCG_Delete]    Script Date: 06/20/2017 15:17:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_CallbackCCG_Delete]
@id int
as
begin
	delete CallbackCCG where ID = @id;
end
GO
/****** Object:  Table [dbo].[CallbackCCGLogs]    Script Date: 06/20/2017 15:17:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CallbackCCGLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MSISDN] [nvarchar](50) NULL,
	[Result] [nvarchar](50) NULL,
	[Reason] [nvarchar](50) NULL,
	[ProductId] [nvarchar](50) NULL,
	[TransID] [nvarchar](50) NULL,
	[TPCGID] [nvarchar](50) NULL,
	[Songname] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_CallbackCCGLogs_Insert]    Script Date: 06/20/2017 15:17:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_CallbackCCGLogs_Insert]
@msisdn nvarchar (50),
@result nvarchar (50),
@reason nvarchar (50),
@productId nvarchar (50),
@transID nvarchar (50),
@tpcgID nvarchar (50),
@songName nvarchar (50)
as
begin
	insert CallbackCCGLogs values (@msisdn, @result, @reason, @productId, @transID, @tpcgID, @songName);
end
GO
