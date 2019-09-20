class AccountIndexPage extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div id="page-container">
        <div className="row">
          <div className="col-md-4">
            <h4>Your Account</h4>
            <hr />

            <div class="form-group">
              <label>Name</label>
              <div>{ this.props.user.name }</div>
            </div>

            <div class="form-group">
              <label>Email</label>
              <div>{ this.props.user.email }</div>
            </div>

            <div class="form-group">
              <label>Joined On</label>
              <div>{ this.props.user.joined_on }</div>
            </div>

          </div>
          <div className="col-md-6 col-md-offset-2">
          </div>
        </div>
      </div>
    );
  }
}
