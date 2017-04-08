
var MessageStatus = React.createClass({
    render: function () {
        return (
        <span className='alert alert-success'>Your message id is {this.props.messageId}</span>
        )
    }
});

var MessageForm = React.createClass ({
    getInitialState: function() {
        return {
            recipients: "",
            subject: "",
            message: "",
            messageId: 0
        };
    },

    handleSubmit:function(e) {
        e.preventDefault();

        var that = this;
        
        var data = new FormData();

        data.append('recipients', this.state.recipients);
        data.append('message', this.state.message);
        data.append('subject', this.state.subject);

        var xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {
                that.setState({ messageId: parseInt(this.responseText) });
            }
        };
        xhr.send(data);

        this.setState({
            recipients: "",
            subject: "",
            message: ""
        });
    },

    handleRecipientsChange:function(e) {
        this.setState({recipients: e.target.value});
    },

    handleSubjectChange: function (e) {
        this.setState({ subject: e.target.value });
    },

    handleMessageChange: function (e) {
        this.setState({ message: e.target.value });
    },

    render: function() {
        return (
                <form onSubmit={this.handleSubmit}>
                  <div className="form-group">
                    <label htmlFor="recipients">Recipients</label>
                    <input type="text" value={this.state.recipients} onChange={this.handleRecipientsChange} className="form-control" id="recipients" placeholder="Recipients" required />
                    (must be separated by semicolon)
                  </div>
                  <div className="form-group">
                    <label htmlFor="subject">Subject</label>
                    <input type="text" value={this.state.subject} onChange={this.handleSubjectChange} className="form-control" id="subject" placeholder="Subject" required />
                  </div>
                  <div className="form-group">
                    <label htmlFor="message">Message</label>
                    <input type="text" value={this.state.message} onChange={this.handleMessageChange} className="form-control" id="message" placeholder="Message" required />
                  </div>
                  <button type="submit" className="btn btn-default">Submit</button>&nbsp;
                  { this.state.messageId > 0 ? <MessageStatus messageId={this.state.messageId}/> : null }
                </form>
            )
    }
});

ReactDOM.render(
  <MessageForm submitUrl="/messages/send" />,
  document.getElementById('content')
);