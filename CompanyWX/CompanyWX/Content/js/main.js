$(function() {
	
	$('.clidj').each(function(index) {
		$(this).click(function() {
			if($(this).hasClass("on")){
        alert("您已点过赞!");
        return false;    	
			};
			$.ajax({
					type: "GET",
					dataType:"text",
					url: "click.asp?id="+$(this).attr("data-id")+"&user="+$(this).attr("data-user"),
					success: function(data) {
							$('.clidj').eq(index).addClass("on");
							$('.clidj').eq(index).find("span").html(data);
					}
			});
			return false;
		});
	});
	
	
	$("#rightMenu").click(function() {
			$('.menu').show(200);
			$(".menua").show();
	});
	$(".menua").click(function() {
		$('.menu').hide(200);
		$(".menua").hide();
	});
	
});

function regyz() {
    if($("#email").val()==''){
        alert("邮箱不能为空");
        return false;    
    }
    if(!$("#email").val().match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) { 
        alert("邮箱格式不正确"); 
        return false; 
    }
    if($("#password").val()==''){
        alert("密码不能为空");
        return false;    
    }
    if($("#password2").val()==''){
        alert("确认密码不能为空");
        return false;    
    }
//    if($("#yqm").val()==''){
//        alert("邀请码不能为空");
//        return false;    
//    }
//    if($("#yqm").val().length!=16){
//        alert("邀请码不正确");
//        return false;    
//    }
    if($("#yzm").val()==''){
        alert("验证码不能为空");
        return false;    
    }
    if($("#yzm").val().length!=4){
        alert("验证码不正确");
        return false;    
    }
    
    if($("#reemail").val()!=$("#email").val()){
        alert("两次邮箱输入不一致，请重新输入");
        return false;    
    }
    if($("#password").val()!=$("#password2").val()){
        alert("两次密码输入不一致，请重新输入");
				$("#password").val("");
				$("#password2").val("");
        return false;    
    }

};

function loginyz() {
    if($("#email").val()==''){
        alert("邮箱不能为空");
        return false;    
    }
    if(!$("#email").val().match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) { 
        alert("邮箱格式不正确"); 
        return false; 
    }
    if($("#password").val()==''){
        alert("密码不能为空");
        return false;    
    }
    if($("#yzm").val()==''){
        alert("验证码不能为空");
        return false;    
    }
    if($("#yzm").val().length!=4){
        alert("验证码不正确");
        return false;    
    }
};
function yqmyz() {
//    if($("#yqm").val()==''){
//        alert("邀请码不能为空");
//        return false;    
//    }
//    if($("#yqm").val().length!=16){
//        alert("邀请码不正确");
//        return false;    
//    }

};
function syyqm() {
    if($("#email2").val()==''){
        alert("邮箱不能为空");
        return false;    
    }
    if(!$("#email2").val().match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) { 
        alert("邮箱格式不正确"); 
        return false; 
    }
//    if($("#yqm2").val()==''){
//        alert("邀请码不能为空");
//        return false;    
//    }
//    if($("#yqm2").val().length!=16){
//        alert("邀请码不正确");
//        return false;    
//    }
};
function plyz() {
    if($("#body").val()==''){
        alert("评论内容不能为空");
        return false;    
    }
    if($("#yzm").val().length!=4){
        alert("验证码不正确");
        return false;    
    }
};