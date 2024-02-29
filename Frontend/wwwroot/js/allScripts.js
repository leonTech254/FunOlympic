function toogle_menu()
{
    let user_document=document.querySelector(".nav_menu");
    user_document.classList.toggle("open_menu");
    
}



function toggle_side_bar()
{
    let user_document=document.getElementById("side_bar");
    user_document.classList.toggle("open_side_menu");
}


function showToastMessage(message)
{
  // alert("hello world")
 
    toastr.success(message)
}

function show_chart_map(TotalBids, bidWon, Failed,active) {

  const ctx = document.getElementById('myChart');

  const data = {
    labels: [
      'Total bids',
      'Bid won',
      'Failed bid',
      'active bid',
    ],
    datasets: [{
      label: 'My First Dataset',
      data: [TotalBids, bidWon, Failed,active],
      backgroundColor: [
        'orange',
        'green',
        'red',
        'blue'
      ],
      hoverOffset: 4
    }]
  };

  new Chart(ctx, {
    type: 'pie',
    data: data
  });


}



function barChartSummury(totalBids, bidWon, failedBids, activeBids) {
  const data = {
    labels: ['Total Bids', 'Bid Won', 'Failed Bids', 'Active Bids'],
    datasets: [
      {
        label: 'Bids Summary',
        backgroundColor: ['orange', 'green', 'red', 'blue'],
        borderColor: 'rgba(75, 192, 192, 1)',
        borderWidth: 1,
        stack: 'combined',
        data: [totalBids, bidWon, failedBids, activeBids],
      },
    ],
  };

  const config = {
    type: 'bar',
    data: data,
    options: {
      plugins: {
        title: {
          display: true,
          text: 'Bids Summary',
        },
      },
      scales: {
        y: {
          stacked: true,
          beginAtZero: true,
        },
        x: {
          stacked: true,
        },
      },
    },
  };

  const chart = new Chart(document.getElementById('bar_graph'), config);
}

// Call this function in your Blazor component with the BidStatiscs data
function showBidStatisticsOnChart(totalBids, bidWon, failedBids, activeBids) {
  show_chart_map(totalBids, bidWon, failedBids, activeBids);
  barChartSummury(totalBids, bidWon, failedBids, activeBids);
}

  
  
function uploadImage() {
  const txtpostimg1 = document.getElementById('IamgeUrl');
  let txtpostimg = "https://res.cloudinary.com/du2zb0o1q/image/upload/v1692710889/cld-sample.jpg";

  return new Promise((resolve, reject) => {
    const files = txtpostimg1.files;

    if (files) {
      const formData = new FormData();
      formData.append("file", files[0]);
      formData.append("upload_preset", "auctionSystem");
      formData.append("cloud_name", "du2zb0o1q");

      fetch('https://api.cloudinary.com/v1_1/du2zb0o1q/image/upload', {
        method: "POST",
        body: formData
      })
      .then((res) => res.json())
      .then((res) => {
        txtpostimg = res.url;
        resolve(txtpostimg);
      })
      .catch((error) => {
        reject(error);
      });
    } else {
      reject("No files selected");
    }
  });
}

function uploadImageNew()
{
  const txtpostimg1 = document.getElementById('IamgeUrlNew');
  let txtpostimg = "https://res.cloudinary.com/du2zb0o1q/image/upload/v1692710889/cld-sample.jpg";

  return new Promise((resolve, reject) => {
    const files = txtpostimg1.files;

    if (files) {
      const formData = new FormData();
      formData.append("file", files[0]);
      formData.append("upload_preset", "auctionSystem");
      formData.append("cloud_name", "du2zb0o1q");

      fetch('https://api.cloudinary.com/v1_1/du2zb0o1q/image/upload', {
        method: "POST",
        body: formData
      })
      .then((res) => res.json())
      .then((res) => {
        txtpostimg = res.url;
        resolve(txtpostimg);
      })
      .catch((error) => {
        reject(error);
      });
    } else {
      reject("No files selected");
    }
  });

}




function show_sweet_alert(message,title,type)
{
  Swal.fire({
    title:title,
    text: message,
    icon: type
  });
}