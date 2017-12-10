var app = new Vue({
    el: '#app',
    data: {
        isLoading: false,
        names: [],
        days: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday']
    },
    created() {
        this.refreshData()
    },
    methods: {
        refreshData() {
            let _this = this
            _this.isLoading = true
            axios.get('http://localhost:64563/api/fate')//http://wof.wannabedev.com/api/fate')
                .then(function (response) {
                    console.log(response)
                    _this.names.length = 0 // clear existing data
                    _this.names = []
                    let weekendCount = 0;
                    for (let i = 0; i < response.data.length + weekendCount; i++) {
                        if (i == 5) {
                            // weekend, skip 
                            weekendCount++;
                            _this.names.push(['Weekend', '', ''])
                            _this.names.push(['Weekend', '', ''])
                        } else {
                            _this.names.push([_this.days[(i - weekendCount) % 5], response.data[i - weekendCount][0], response.data[i - weekendCount][1]])
                        }
                    }
                    _this.names.push(['Weekend', '', ''])
                    _this.names.push(['Weekend', '', ''])
                    _this.isLoading = false
                })
                .catch(function (error) {
                    console.log(error)
                    _this.isLoading = false
                })
        }
    }
})