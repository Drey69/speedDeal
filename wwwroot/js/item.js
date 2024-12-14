
var objprog = {
    data() {
      return { 
        ccolor: 'red',
        cwidth: 70,
        cheight: 70,
        ctop: 250,
        cleft: 250,
        ismd: false,
        name: "",
        link:'',
        mdx:0,
        mdy:0,
        zInd:100
      }
    },
    computed: {
      itemStyle(){
        return{
          'background-color': this.ccolor,
          width: this.cwidth + 'px',
          height: this.cheight + 'px',
          top: this.ctop + 'px',
          left: this.cleft + 'px',
          position:"absolute",
          'user-select': "none",
          'z-index':this.zInd
        }
      }
    },
    methods: {
      divmousedown(event) {
        if(event.button != 0) return;
        this.mdy = event.offsetY;
        this.mdx = event.offsetX;
        this.ismd = true;
        this.zInd = 900;
        $(".paa").css("background-color", "#bbff00");
        console.log("mouse Down");
      },
  
      divmouseup(event){
        if(event.button != 0) return;
        this.ismd = false;
        console.log("mouseUp"); 
        this.zInd = 900;  
      },
  
      divmousemove(event){
        if(event.button != 0) return;
        if(!this.ismd) return;
        this.ctop = event.clientY - this.mdy;
        this.cleft = event.clientX - this.mdx;
      }
    },
    created() {
      // `this` указывает на экземпляр vm
      console.log('счётчик: ' + this.count) // => "счётчик: 1"
    }
  
  };