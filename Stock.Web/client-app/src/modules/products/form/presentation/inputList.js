import React from "react";
import { Field } from "redux-form";
import api from "../../../../common/api";
import SelectField from "../../../../components/inputs/SelectField";

export default class InputList extends React.Component {
  _isMounted = false;

  constructor() {
    super();
    this.state = {
      options: [],
    };
  }

  componentDidMount() {
    this._isMounted = true;

    if (this.props.url !== undefined) {
      return api
        .get(this.props.url)
        .then((response) => {
          if (this._isMounted) {
            response.data.forEach((element) => {
              this.setState({
                options: this.state.options.concat([
                  { label: element[this.props.param], value: element.id },
                ]),
              });
            });
          }
        })
        .catch(() => {
          return null;
        });
    }
  }

  componentWillUnmount() {
    this._isMounted = false;
  }

  render() {
    return (
      <Field
        label={this.props.label}
        options={this.state.options}
        name={this.props.name}
        component={SelectField}
      ></Field>
    );
  }
}
