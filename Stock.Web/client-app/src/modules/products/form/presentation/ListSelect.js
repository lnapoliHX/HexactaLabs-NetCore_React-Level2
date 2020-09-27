import React, { Component } from "react";
import { Field } from "redux-form";
import api from "../../../../common/api";
import SelectField from "../../../../components/inputs/SelectField";

class ListSelect extends Component {
  _isMounted = false;
  state = {
    options: [],
  };

  componentDidMount() {
    this._isMounted = true;
    api
      .get("/productType")
      .then((response) => {
        if (this._isMounted) {
          this.setState({
            options: this.state.options.concat([
              { value: "", label: "Desplegar lista categorías disponibles 🠒" },
            ]),
          });
          let select = response.data.map((one) => {
            return {
              value: one.id,
              label: one.description,
            };
          });
          this.setState({ options: this.state.options.concat(select) });
        }
      })
      .catch(() => {
        return null;
      });
  }

  componentWillUnmount() {
    this._isMounted = false;
  }

  render() {
    return (
      <Field
        label="Categorías"
        options={this.state.options}
        name="productTypeId"
        component={SelectField}
        type="select"
      ></Field>
    );
  }
}

export default ListSelect;
