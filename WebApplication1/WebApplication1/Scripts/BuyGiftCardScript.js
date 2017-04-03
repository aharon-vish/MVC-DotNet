/// <reference path="jquery-2.1.4.js" />

    $(document).ready(function () {
        $('#rootwizard').bootstrapWizard({
            onTabShow: function (tab, navigation, index) {
                var $total = navigation.find('li').length;
                var $current = index + 1;
                var $percent = ($current / $total) * 100;
                $('#rootwizard').find('.bar').css({ width: $percent + '%' });

                // If it's the last tab then hide the last button and show the finish instead
                if ($current >= $total) {
                    $('#rootwizard').find('.pager .next').hide();
                    $('#rootwizard').find('.pager .finish').show();
                    $('#rootwizard').find('.pager .finish').removeClass('disabled');
                } else {
                    $('#rootwizard').find('.pager .next').show();
                    $('#rootwizard').find('.pager .finish').hide();
                }

            }
        });
        $('#rootwizard .finish').click(function () {
            alert('Finished!, Starting over!');
            $('#rootwizard').find("a[href*='tab1']").trigger('click');
        });
    });
$('#rootwizard .finish').click(function () {
    alert('Finished!, Starting over!');
    $('#rootwizard').find("a[href*='tab1']").trigger('click');
});

$(document).ready(function () {

    var WidthOfDrowbord = $('#card').width();
    var defaultBoard = new DrawingBoard.Board('default-board');
    $("#email").css("border", "none");
    $('#email').prop('readonly', true);

    $("#FullName").css("border", "none");
    $('#FullName').prop('readonly', true);

    $("#credit").css("border", "none");
    $('#credit').prop('readonly', true);

    $("#greeting").css("border", "none");
    $('#greeting').prop('readonly', true);

    var d = new Date();
    var m = d.getMonth();
    var y = d.getFullYear();
    document.getElementById("ValidOnCard").innerHTML = "Valid Card: " + (d.getMonth() + 1) + "/" + (y + 1);
    var NumCard = Math.floor((Math.random() * 1000000) + 10000);
    CheckIfNuberIdCardExist();

    function CheckIfNuberIdCardExist() {
        $.get("@Url.Action("doesIdCardExist")",
           {
               IdCard: NumCard
           }, function (data) {
               if (data == "true") {

                   NumCard = Math.floor((Math.random() * 1000000) + 10000);
                   document.getElementById("GiftCardID").innerHTML = "Card Number: " + NumCard;

               }
               else {

                   NumCard = Math.floor((Math.random() * 1000000) + 10000);
                   CheckIfNuberIdCardExist();
               }

           }

          );

        // end ajax
    }
});
$("#FilUploader").change(function () {
    var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        alert("Only '.jpeg','.jpg', '.png', '.gif', '.bmp' formats are allowed.");
        $('#FilUploadegdf').val('');
    }
});
function Sketch() {
    document.getElementById('Sketch').style.display = "block";
    document.getElementById('RecordVoice').style.display = "none";
    document.getElementById('TextGreet').style.display = "none";
}
function RecordVoice() {
    //window.open('/Home/CreatePartialView', '_blank', 'left=100,top=100,width=400,height=300,toolbar=1,resizable=0');
    window.open('ViewRecVoice');
    @*BootstrapDialog.show({
        message: $('<div></div>').load('@Url.Action("ViewRecVoice", "Home")')
    });*@
    var canvas = document.getElementById('GreetingImg');
    canvas.width = canvas.width;
    document.getElementById('RecordVoice').style.display = "block";
    document.getElementById('Sketch').style.display = "none";
    document.getElementById('TextGreet').style.display = "none";

}
function TextGreet() {
    var canvas = document.getElementById('GreetingImg');
    canvas.width = canvas.width;
    document.getElementById('TextGreet').style.display = "block";
    document.getElementById('RecordVoice').style.display = "none";
    document.getElementById('Sketch').style.display = "none";
}
$("#Email").bind("keyup input paste", function () {
    var Email = $(this).val();
    $("#emailOnCard").text(Email);
    $("#email").val(Email);
});
$("#FirstName").bind("keyup input paste", function () {
    var FirstName = $(this).val();
    var LastName = $("#LastName").val();
    var FullName = FirstName + ' ' + LastName;
    $("#FullNameOnCard").text(FullName);
    $("#FullName").val(FullName);
});

$("#LastName").bind("keyup input paste", function () {
    var LastName = $(this).val();
    var FirstName = $("#FirstName").val();
    var FullName = FirstName + ' ' + LastName;
    $("#FullNameOnCard").text(FullName);
    $("#FullName").val(FullName);
});
$("#Credit").bind("keyup input paste", function () {
    var Credit = $(this).val();
    var creditOnCard = $(this).val();
    Credit += "$" + " for " + '@ViewData["NameOfStroe"]'; //tow display one on credit card and the second for final
    creditOnCard += "$";
    $("#CreditOnCard").text(creditOnCard);
    $("#credit").val(Credit.toString());
});
$("#FinshBtn").click(function () {
    var Greetingdiv = document.getElementById('Greeting');
    Greetingdiv.appendChild(link);

    if ($('#Sketch').css('display') == 'block') {
        alert("Sketch");
    }
    if ($('#RecordVoice').css('display') == 'block') {
        alert("RecordVoice");
    }

});

var NewLinkSketch = 0;

$("div.Sketch")
  .mouseout(function () {
      html2canvas([document.getElementById('GreetingImg')], {
          onrendered: function (canvas) {
              //  document.body.appendChild(canvas);
              var GreeDiv = document.getElementById('Sketch');
              var link = document.createElement('a');
              link.id = 'link';
              link.innerHTML = 'download image';

              link.addEventListener('click', function (ev) {
                  link.href = canvas.toDataURL();
                  link.download = "mypainting.png";
                  document.getElementById('Sketch').style.display = "none";

              }, false);
              if (NewLinkSketch == 0) {
                  GreeDiv.appendChild(link);
                  NewLinkSketch = 1;
              }
              else {
                  var linkA = document.getElementById('link')
                  document.getElementById('link').remove();
                  GreeDiv.appendChild(link);
              }
          }


      });

  });

var NewLinkText = 0;

$("div.Editor-editor")
  .mouseout(function () {

      html2canvas([document.getElementById('TextEditor')], {
          onrendered: function (canvas) {
              //  document.body.appendChild(canvas);
              var GreeDiv = document.getElementById('TextGreet');
              var link = document.createElement('a');
              link.id = 'link';
              link.innerHTML = 'download image';

              link.addEventListener('click', function (ev) {
                  link.href = canvas.toDataURL();
                  link.download = "mypainting.png";
                  document.getElementById('Sketch').style.display = "none";

              }, false);
              if (NewLinkText == 0) {
                  GreeDiv.appendChild(link);
                  NewLinkText = 1;
              }
              else {
                  var linkA = document.getElementById('link')
                  document.getElementById('link').remove();
                  GreeDiv.appendChild(link);
              }
          }


      });

  });



