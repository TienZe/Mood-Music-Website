document.addEventListener('DOMContentLoaded', function () {
    const audioElement = document.getElementById('audio-player');
    console.log(audioElement);


    let audioContext, analyser, source, bufferLength, dataArray;

    audioElement.addEventListener('play', function () {
        console.log('canplay');
        if (!audioContext) {
            // Tạo AudioContext và Analyser
            audioContext = new (window.AudioContext || window.webkitAudioContext)();
            analyser = audioContext.createAnalyser();
            if (!source) {
                source = audioContext.createMediaElementSource(audioElement);
                source.connect(analyser);
            }
            analyser.connect(audioContext.destination);
            // Cấu hình và lấy dữ liệu âm thanh
            analyser.fftSize = 128;
            bufferLength = analyser.frequencyBinCount;
            dataArray = new Uint8Array(bufferLength);
        }

        console.log(audioContext);
        console.log(analyser);
        console.log(source);

        drawRectangles();
    });

    let animationId;

    //======== Thêm html các hình chữ nhật ==========
    const rectanglesContainer = document.getElementById('rectangles-container');
    // Số lượng rectangles cần thiết
    const numRectangles = 28;
    // Tạo các rectangles và thêm vào container
    for (let i = 0; i < numRectangles; i++) {
        const rectangle = document.createElement('div');
        rectangle.classList.add('rectangle');
        rectanglesContainer.appendChild(rectangle);
    }
    const rectangles = document.querySelectorAll('.rectangle');
    console.log(rectangles);

    // Hàm để vẽ hiệu ứng thanh chữ nhật
    function drawRectangles() {
        if (!analyser) return;
        analyser.getByteFrequencyData(dataArray);
        for (let i = 0; i < numRectangles; i++) {
            const value = dataArray[i + 5];
            const rectangle = rectangles[i];

            // Áp dụng giá trị từ dữ liệu âm thanh để điều chỉnh chiều cao của thanh chữ nhật
            const height = (value / 255.0) * rectanglesContainer.clientHeight + 'px';
            rectangle.style.height = height;
        }
        animationId = requestAnimationFrame(drawRectangles);
    }

    audioElement.addEventListener('play', function () {
        console.log('play');
        drawRectangles();
    });

    audioElement.addEventListener('pause', function () {
        cancelAnimationFrame(animationId);
        for (let i = 0; i < numRectangles; i++) {
            const rectangle = rectangles[i];
            rectangle.style.animation = 'none';
        }
    });
});


