class AccountRegisterPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      validation_messages: [],
      email: '',
      name: '',
      password: '',
      password_confirmation: ''
    };

    this.inputChange = this.inputChange.bind(this);
    this.register = this.register.bind(this);
  }

  inputChange(input, value) {
    let state = {};
    state[input] = value;
    this.setState(state);
  }

  register() {
    const { email, name, password, password_confirmation } = this.state;
    const data = { email, name, password, password_confirmation };
    BOS.UTIL.FETCH
      .post(this.props.urls.api.account_register, data)
      .then((response) => {
        if(!response.action_success) {
          this.setState({
            validation_messages: response.messages
          });
          return;
        }
        window.location.href = this.props.urls.pages.account_index;
      });
  }

  render() {
    const { validation_messages } = this.state;

    return (
      <div id="page-container">
        <div className="row">
          <div className="col-md-4">
            <h4>Create Account</h4>
            <hr />

            <div className="form-group">
              <label htmlFor="email">Email</label>
              <input type="text" id="email" className="form-control"
                value={ this.state.email }
                onChange={ (event) => this.inputChange('email', event.target.value) }
              />
            </div>
            <div className="form-group">
              <label htmlFor="name">Name</label>
              <input type="text" id="name" className="form-control"
                value={ this.state.name }
                onChange={ (event) => this.inputChange('name', event.target.value) }
              />
            </div>
            <div className="form-group">
              <label htmlFor="password">Password</label>
              <input type="password" id="password" className="form-control"
                value={ this.state.password }
                onChange={ (event) => this.inputChange('password', event.target.value) }
              />
            </div>
            <div className="form-group">
              <label htmlFor="password_confirmation">Confirm Password</label>
              <input type="password" id="password_confirmation" className="form-control"
                value={ this.state.password_confirmation }
                onChange={ (event) => this.inputChange('password_confirmation', event.target.value) }
              />
            </div>

            <ValidationMessages validation_messages={ validation_messages } />

            <button className="btn btn-primary"
              onClick={ this.register }
            >Register</button>
          </div>
          <div className="col-md-6 col-md-offset-2">
          </div>
        </div>
      </div>
    );
  }
}
